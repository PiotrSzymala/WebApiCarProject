import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule]
})
export class RegisterComponent {
  registerForm = {
    username: '',
    password: '',
    email: ''
  };

  constructor(private authService: AuthService) { }

  register() {
    this.authService.register(this.registerForm).subscribe(response => {
      console.log('Registration successful', response);
    }, error => {
      console.error('Registration failed', error);
    });
  }
}
