import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [CommonModule]
})
export class HomeComponent {
  constructor(private authService: AuthService, private router: Router) { }

  logout() {
    this.authService.logout().subscribe(response => {
      console.log('Logout successful', response);
      this.authService.clearToken();
      this.router.navigate(['/login']);
    }, error => {
      console.error('Logout failed', error);
    });
  }
}
