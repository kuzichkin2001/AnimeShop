import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorizationService } from 'src/app/services/authorization.service';
import { Nullable } from 'src/app/types/nullable';
import { User } from 'src/app/types/user';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  public userData$: Nullable<Observable<User>> = null;
  public access_token: Nullable<string> = null;

  constructor(public authorizationService: AuthorizationService) {
    console.log('Application started');
  }

  ngOnInit(): void {
    this.access_token = this.authorizationService.getUser();

    if (!this.access_token) {
      this.userData$ = this.authorizationService.authorize();
    }
  }


}
