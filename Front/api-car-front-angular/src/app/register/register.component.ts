import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Ensure FormsModule is imported
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule]
})
export class RegisterComponent {
  registerForm = {
    login: '',
    passwd: ''
  };

  constructor(private authService: AuthService, private router: Router) { }

  register() {
    this.authService.register(this.registerForm).subscribe(response => {
      console.log('Registration successful', response);
      this.router.navigate(['/login']);  // Navigate to login page on successful registration
    }, error => {
      console.error('Registration failed', error);
    });
  }

  navigateToLogin() {
    this.router.navigate(['/login']);  // Navigate to the login page
  }
}