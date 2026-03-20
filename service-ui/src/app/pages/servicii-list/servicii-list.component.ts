import { Component, OnInit, inject } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { ApiService, ServiciuDto } from '../../services/api.service';

@Component({
  selector: 'app-servicii-list',
  standalone: true,
  templateUrl: './servicii-list.component.html',
  styleUrls: ['./servicii-list.component.css'],
  imports: [NgFor, NgIf]
})
export class ServiciiListComponent implements OnInit {
  private api = inject(ApiService);

  loading = true;
  error = '';
  servicii: ServiciuDto[] = [];

  ngOnInit() {
    this.api.getServicii().subscribe({
      next: data => {
        this.servicii = data;   // <-- FĂRĂ picsum, FĂRĂ map
        this.loading = false;
      },
      error: err => {
        console.error(err);
        this.error = 'Nu am putut încărca serviciile.';
        this.loading = false;
      }
    });
  }
}
