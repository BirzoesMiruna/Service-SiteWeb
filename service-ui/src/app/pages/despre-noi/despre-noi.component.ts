import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-despre-noi',
  standalone: true,
  imports: [CommonModule, RouterLink],   // <— important!
  templateUrl: './despre-noi.component.html',
  styleUrls: ['./despre-noi.component.css']
})
export class DespreNoiComponent {}
