import { LocalizedString } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { faCoffee, faGear, faHeartCircleCheck, faList, faMessage, faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../services/auth.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit { 
  
  // icons
  faCoffee = faCoffee;
  faHeartCircleCheck =faHeartCircleCheck;
  faList = faList;
  faMessage = faMessage;
  faGear = faGear;
  faRightFromBracket = faRightFromBracket;
  //

  logins: any = {};
  loggedIn = false;

  constructor(private authService: AuthService ) {
    
  }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.logins).subscribe({
      next: () => {
        this.loggedIn = !! localStorage.getItem('token');
        console.log('login successful !')
      },
      error: () => console.log('login failed !'),
      complete: () => console.log('End of response !')
    });
  }

  logout() {
    localStorage.removeItem('token');
    console.log('logged out')
  }

}
