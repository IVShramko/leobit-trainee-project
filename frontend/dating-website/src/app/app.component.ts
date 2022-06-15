import { NavigationEnd, Router, RouterEvent } from '@angular/router';
import { BehaviorSubject, filter, Observable, Subject, takeLast } from 'rxjs';
import { UserService } from 'src/app/services/user-service/user.service';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { Component, Output } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'dating-website';

  constructor(private authService: AuthService,
    private userService: UserService, private router: Router) 
  {}

  IsAuthenticated = new Observable<boolean>();

  ngOnInit(): void {
    this.IsAuthenticated = this.authService.AuthenticationStatus$
  }
}
