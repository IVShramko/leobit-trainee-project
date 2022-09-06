import { StatusService } from './services/status-service/status.service';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { Component, OnInit, HostListener } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'dating-website';

  IsAuthenticated = new Observable<boolean>();

  // @HostListener('window:beforeunload')
  // BeforeUnloadHandler() { 
  //   this.statusService.UpdateUserStatus(false).subscribe();
  // }

  constructor(private authService: AuthService,
    private statusService: StatusService) {
  }

  ngOnInit(): void {
    this.IsAuthenticated = this.authService.AuthenticationStatus$;
    this.statusService.GetUserStatus().subscribe(
      (result) => console.log(result)
    );
  }
}
