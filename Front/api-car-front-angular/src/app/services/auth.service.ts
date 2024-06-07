import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7096/api/Auth';

  constructor(private http: HttpClient) { }

  register(registerForm: { login: string; passwd: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/Register`, registerForm).pipe(
      catchError(this.handleError)
    );
  }

  login(loginForm: { login: string; passwd: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/Login`, loginForm).pipe(
      catchError(this.handleError)
    );
  }

  logout(): Observable<any> {
    return this.http.post(`${this.apiUrl}/Logout`, {}).pipe(
      catchError(this.handleError)
    );
  }

  clearToken() {
    localStorage.removeItem('token');
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return token != null;
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      if (error.error && error.error.Errors) {
        errorMessage += `\nErrors: ${error.error.Errors.join(', ')}`;
      }
    }
    return throwError(errorMessage);
  }
}
