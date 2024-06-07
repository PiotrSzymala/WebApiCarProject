import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),  // Provides HttpClientModule
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,  // Provide JwtHelperService
    ...appConfig.providers,
  ]
})
.catch(err => console.error(err));
