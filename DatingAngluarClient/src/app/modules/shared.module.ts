import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FileUploadModule } from 'ng2-file-upload';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { TimeagoModule } from 'ngx-timeago';



@NgModule({
  declarations: [],
  imports:[
    CommonModule,
    FontAwesomeModule,
    FileUploadModule,
    BsDatepickerModule.forRoot(), 
    TabsModule.forRoot(),
    NgxGalleryModule,
    ButtonsModule.forRoot(),
    PaginationModule.forRoot(),
    TimeagoModule.forRoot(),
    NgxSpinnerModule.forRoot({ type: 'ball-spin-clockwise-fade-rotating' }),    
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })
  ],
  exports:[
    FontAwesomeModule,
    ToastrModule,
    NgxGalleryModule,
    NgxSpinnerModule,
    TimeagoModule,
    ButtonsModule,
    FileUploadModule,
    PaginationModule,
    BsDatepickerModule,  
    TabsModule 
  ]
})
export class SharedModule { }
