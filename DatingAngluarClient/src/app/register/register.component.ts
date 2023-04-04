import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  
  constructor(private authService: AuthService, private toastr: ToastrService ) { }

  ngOnInit(): void {
  }

  registers:any = {};

  register() {
    this.authService.register(this.registers).subscribe({
      next: (resp: any) => {
        this.toastr.success(resp.message, resp.status);
        this.cancel();
      },
      error: err => {
        console.log(err);
        this.toastr.error(err.error.message);
      }
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
