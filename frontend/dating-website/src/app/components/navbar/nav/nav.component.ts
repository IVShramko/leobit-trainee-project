import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user-service/user.service';
import { ProfileMainDTO } from 'src/app/models/profileMainDTO';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {

  MainProfile = new Observable<ProfileMainDTO>();
  @Input() IsAuthenticated: boolean | null;

  constructor(public authService: AuthService,
    private router: Router,
    private userService: UserService) {
  }

  ngOnChanges() {
    this.LoadMainProfile();
    this.IsAuthenticated = this.authService.Authenticate();
  }

  private LoadMainProfile() {
    this.MainProfile = this.userService.GetMainProfile();
  }

  LogOut() {
    this.authService.LogOut()
      .subscribe((result) => {
        this.IsAuthenticated = false;
        if(result) this.router.navigate(['/unauthorized']);
      });
  }
  
}
