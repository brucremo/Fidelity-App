import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.scss']
})
export class AuthCallbackComponent implements OnInit {

  error: boolean;

  constructor(private authService: AuthenticationService, private router: Router, private route: ActivatedRoute) {}

  async ngOnInit() {

    // check for error
    if (this.route.snapshot.fragment.indexOf('error') >= 0) {
       this.error=true;
       return;
     }
  }
}
