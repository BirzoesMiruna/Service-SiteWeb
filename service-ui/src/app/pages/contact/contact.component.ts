import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {
  form: FormGroup;
  submitting = false;
  success = '';
  error = '';

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      nume: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      telefon: [''],
      mesaj: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(1000)]]
    });
  }

  get f() { return this.form.controls; }

  async submit() {
    this.success = '';
    this.error = '';
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }

    this.submitting = true;
    try {
      // TODO: trimite pe API-ul tău (ex: /api/contact)
      await new Promise(r => setTimeout(r, 900)); // simulare request
      this.success = 'Mulțumim! Ți-am primit mesajul și te contactăm în scurt timp.';
      this.form.reset();
    } catch {
      this.error = 'Ceva n-a mers. Te rugăm încearcă din nou.';
    } finally {
      this.submitting = false;
    }
  }
}
