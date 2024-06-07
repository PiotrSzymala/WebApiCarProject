import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Car } from '../models/car.model';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private carApiUrl = 'https://localhost:7096/api/CarManagement';

  constructor(private http: HttpClient) { }

  getCars(): Observable<Car[]> {
    return this.http.get<Car[]>(this.carApiUrl).pipe(
      catchError(this.handleError)
    );
  }

  deleteCar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.carApiUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }

  addCar(car: Car): Observable<Car> {
    return this.http.post<Car>(this.carApiUrl, car).pipe(
      catchError(this.handleError)
    );
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
