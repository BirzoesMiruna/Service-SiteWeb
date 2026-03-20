import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { DespreNoiComponent } from './pages/despre-noi/despre-noi.component';
import { ServiciiListComponent } from './pages/servicii-list/servicii-list.component';
import { RecenziiComponent } from './pages/recenzii/recenzii.component';
import { ContactComponent } from './pages/contact/contact.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'despre-noi', component: DespreNoiComponent },
  { path: 'servicii', component: ServiciiListComponent },
  { path: 'recenzii', component: RecenziiComponent },
  { path: 'contact', component: ContactComponent },
  { path: '**', redirectTo: '' } // fallback
];
