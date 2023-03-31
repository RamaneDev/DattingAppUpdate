import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  registers:any = {};

  register() {
    this.authService.register(this.registers).subscribe({
      next: resp => console.log(resp),  // TODO : navigate to successful created user page !
      error: err => console.log(err)
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
