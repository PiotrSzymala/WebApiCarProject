import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { CarService } from '../services/car.service';
import { Car } from '../models/car.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [CommonModule]
})
export class HomeComponent implements OnInit {
  cars: Car[] = [];

  constructor(
    private authService: AuthService,
    private carService: CarService,
    private router: Router
  ) { }

  ngOnInit() {
    this.fetchCars();
  }

  fetchCars() {
    this.carService.getCars().subscribe(cars => {
      this.cars = cars;
    }, error => {
      console.error('Error fetching cars:', error);
    });
  }

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
