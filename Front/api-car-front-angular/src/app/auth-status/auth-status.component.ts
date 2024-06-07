import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-auth-status',
  templateUrl: './auth-status.component.html',
  styleUrls: ['./auth-status.component.css']
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
