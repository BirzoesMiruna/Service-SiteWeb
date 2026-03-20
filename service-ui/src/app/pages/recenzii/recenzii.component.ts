import { Component, OnInit, inject } from '@angular/core';
import { NgFor, NgIf, DatePipe } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ApiService, RecenzieDto, ServiciuDto } from '../../services/api.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-recenzii',
  standalone: true,
  templateUrl: './recenzii.component.html',
  styleUrls: ['./recenzii.component.css'],
  imports: [NgFor, NgIf, DatePipe, ReactiveFormsModule]
})
export class RecenziiComponent implements OnInit {
  private api = inject(ApiService);
  private fb  = inject(FormBuilder);

  loading = true;
  error = '';
  success = '';

  recenzii: RecenzieDto[] = [];
  servicii: ServiciuDto[] = [];
  serviciiMap = new Map<string, string>(); // id => nume

  // -------- pentru formular
  showForm = false;
  submitting = false;
  form = this.fb.group({
    serviciuId: ['', Validators.required],
    rating: [5, [Validators.required, Validators.min(1), Validators.max(5)]],
    text: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(800)]],
  });

  ngOnInit() {
    forkJoin({
      recenzii: this.api.getRecenzii(),
      servicii: this.api.getServicii()
    }).subscribe({
      next: ({ recenzii, servicii }) => {
        this.recenzii = recenzii;
        this.servicii = servicii;
        servicii.forEach(s => this.serviciiMap.set(s.id, s.nume));
        this.loading = false;
      },
      error: err => { console.error(err); this.error = 'Nu am putut încărca recenziile.'; this.loading = false; }
    });
  }

  serviceName(id: string) {
    return this.serviciiMap.get(id) ?? 'Serviciu necunoscut';
  }
  stars(n: number) { return Array(Math.max(0, Math.min(5, Math.round(n)))).fill(0); }

  toggleForm() { this.showForm = !this.showForm; }

  submit() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.submitting = true; this.success = ''; this.error = '';

    this.api.postRecenzie(this.form.getRawValue() as any).subscribe({
      next: () => {
        this.success = 'Mulțumim! Recenzia a fost trimisă.';
        this.form.reset({ serviciuId: '', rating: 5, text: '' });
        this.showForm = false;
        // reîncarcă lista
        this.api.getRecenzii().subscribe(r => this.recenzii = r);
        this.submitting = false;
      },
      error: err => { console.error(err); this.error = 'Nu am putut trimite recenzia.'; this.submitting = false; }
    });
  }
}
