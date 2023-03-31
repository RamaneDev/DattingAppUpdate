import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  modeRegister = false;
  constructor() { }

  ngOnInit(): void {
  }

  registers:any = {}


  ToggleRegister() {
    this.modeRegister = !this.modeRegister;
  }

  cancelRegisterMode(event: boolean) {
    this.modeRegister = event;
  }

}
