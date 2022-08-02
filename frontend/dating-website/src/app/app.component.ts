import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'dating-website';

  constructor(private authService: AuthService) { }

  IsAuthenticated = new Observable<boolean>();

  ngOnInit(): void {
    this.IsAuthenticated = this.authService.AuthenticationStatus$
  }
}
