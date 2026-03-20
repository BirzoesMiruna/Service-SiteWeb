import { Component, OnInit, inject } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ApiService, ServiciuDto, RecenzieDto } from '../../services/api.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,      // ✅ rezolvă NgIf, NgFor
    RouterLink,        // ✅ pentru routerLink și queryParams
    
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  private api = inject(ApiService);

  serviciiTop: ServiciuDto[] = [];
  recenziiRecente: RecenzieDto[] = [];

  ngOnInit() {
    this.api.getServicii().subscribe(s => this.serviciiTop = s.slice(0, 6));
    this.api.getRecenzii().subscribe(r => this.recenziiRecente = r.slice(0, 3));
  }
}
