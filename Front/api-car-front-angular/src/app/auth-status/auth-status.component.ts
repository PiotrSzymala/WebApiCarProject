import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-auth-status',
  standalone: true,
  templateUrl: './auth-status.component.html',
  styleUrls: ['./auth-status.component.css'],
  imports: [CommonModule]  // Ensure CommonModule is imported
})
export class AuthStatusComponent {
  constructor(private authService: AuthService) { }

  get isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  logout() {
    this.authService.logout().subscribe(() => {
      localStorage.removeItem('token');
      console.log('Logout successful');
    });
  }
}
