import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faCoffee, faGear, faHeartCircleCheck, faList, faMessage, faRightFromBracket, faUser } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit { 
  
  // icons
  faUser = faUser;
  faHeartCircleCheck =faHeartCircleCheck;
  faList = faList;
  faMessage = faMessage;
  faGear = faGear;
  faRightFromBracket = faRightFromBracket;
  //

  logins: any = {};
  // we inject authServcie with public modifier in order to access it in html template
  constructor(public authService: AuthService, private router: Router, private toastr: ToastrService ) {    
  }

  ngOnInit(): void {   
  }

  login() {
    this.authService.login(this.logins).subscribe({
      next: () => this.router.navigateByUrl('/members'),    
      error: (error) => console.log(error)      
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/');    
  }

}
