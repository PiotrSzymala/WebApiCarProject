import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7096/api/Auth';

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  register(registerForm: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Register`, registerForm);
  }

  login(loginForm: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Login`, loginForm);
  }

  logout(): Observable<any> {
    return this.http.post(`${this.apiUrl}/Logout`, {});
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !token && !this.jwtHelper.isTokenExpired(token);
  }
}
