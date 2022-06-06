import { NavigationEnd, Router, RouterEvent } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { filter, Subject } from 'rxjs';
import { UserService } from 'src/app/services/user-service/user.service';
import { AuthService } from 'src/app/services/authService/auth.service';
import { Component, Output } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'dating-website';

  constructor(private authService: AuthService,
    private userService: UserService, private router: Router) 
  {
    router.events.pipe(
      filter((event): event is RouterEvent => event instanceof NavigationEnd))
      .subscribe((event: RouterEvent) => {
        if(event.url === '/')
        {
          this.Authenticate();
        }
      });
  }

  ngOnInit(): void {}

  private _AuthenticationStatus$ = new Subject<boolean>();
  @Output() AuthenticationStatus = this._AuthenticationStatus$.asObservable();

  private Authenticate()
  {
    this.authService.AuthRequest()
      .subscribe(
        (response) => {
          this._AuthenticationStatus$.next(true);
          this.router.navigate(['home']);
        },
        (error: HttpErrorResponse) => {
          this._AuthenticationStatus$.next(false);
          this.router.navigate(['unauthorized']);
        }
    );
  }

}
