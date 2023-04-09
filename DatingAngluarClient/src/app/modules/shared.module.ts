import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { NgxSpinnerModule } from 'ngx-spinner';



@NgModule({
  declarations: [],
  imports:[
    CommonModule,
    FontAwesomeModule,
    BrowserAnimationsModule, 
    TabsModule.forRoot(),
    NgxGalleryModule,
    NgxSpinnerModule.forRoot({ type: 'ball-spin-clockwise-fade-rotating' }),    
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })
  ],
  exports:[
    FontAwesomeModule,
    BrowserAnimationsModule,
    ToastrModule,
    NgxGalleryModule,
    NgxSpinnerModule,  
    TabsModule 
  ]
})
export class SharedModule { }
