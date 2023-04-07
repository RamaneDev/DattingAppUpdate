import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';



@NgModule({
  declarations: [],
  imports:[
    CommonModule,
    FontAwesomeModule,
    BrowserAnimationsModule, 
    TabsModule.forRoot(),
    NgxGalleryModule,  
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })
  ],
  exports:[
    FontAwesomeModule,
    BrowserAnimationsModule,
    ToastrModule,
    NgxGalleryModule,  
    TabsModule 
  ]
})
export class SharedModule { }
