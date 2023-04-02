import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [],
  imports:[
    CommonModule,
    FontAwesomeModule,
    BrowserAnimationsModule,    
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })
  ],
  exports:[
    FontAwesomeModule,
    BrowserAnimationsModule,
    ToastrModule
  ]
})
export class SharedModule { }
