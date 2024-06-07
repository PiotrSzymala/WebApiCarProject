import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule]
})
export class LoginComponent {
  loginForm = {
    login: '',
    passwd: ''
  };

  constructor(private authService: AuthService, private router: Router) { }

  login() {
    this.authService.login(this.loginForm).subscribe(response => {
      console.log('Login successful', response);
      localStorage.setItem('token', response.token);  // Adjust this based on your backend response
      this.router.navigate(['/home']);  // Navigate to home page on successful login
    }, error => {
      console.error('Login failed', error);
    });
  }

  navigateToRegister() {
    this.router.navigate(['/register']);  // Navigate to the registration page
  }
}