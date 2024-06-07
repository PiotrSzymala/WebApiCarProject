import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { CarService } from '../services/car.service';
import { Car } from '../models/car.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [CommonModule, ReactiveFormsModule]
})
export class HomeComponent implements OnInit {
  cars: Car[] = [];
  showForm = false;
  addCarForm: FormGroup;

  constructor(
    private authService: AuthService,
    private carService: CarService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.addCarForm = this.fb.group({
      brand: ['', Validators.required],
      model: ['', Validators.required],
      year: ['', [Validators.required, Validators.min(1900), Validators.max(new Date().getFullYear())]],
      registryPlate: ['', Validators.required],
      vinNumber: ['', Validators.required],
      isAvailable: [false]
    });
  }

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

  deleteCar(id: number) {
    this.carService.deleteCar(id).subscribe(() => {
      this.cars = this.cars.filter(car => car.id !== id);
    }, error => {
      console.error('Error deleting car:', error);
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

  showAddCarForm() {
    this.showForm = true;
  }

  hideAddCarForm() {
    this.showForm = false;
  }

  addCar() {
    if (this.addCarForm.valid) {
      const newCar: Car = {
        id: 0,
        createdAtUtc: new Date().toISOString(),
        ...this.addCarForm.value,
        isAvailable: this.addCarForm.value.isAvailable || false
      };

      this.carService.addCar(newCar).subscribe(car => {
        this.cars.push(car);
        this.addCarForm.reset();
        this.hideAddCarForm();
      }, error => {
        console.error('Error adding car:', error);
      });
    }
  }
}