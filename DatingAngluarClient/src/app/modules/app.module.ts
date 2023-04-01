import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from '../app.component';
import { NavComponent } from '../nav/nav.component';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HomeComponent } from '../home/home.component';
import { RegisterComponent } from '../register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { ListsComponent } from '../lists/lists.component';
import { MemberDetailComponent } from '../members/member-detail/member-detail.component';
import { MemberListComponent } from '../members/member-list/member-list.component';
import { MessagesComponent } from '../messages/messages.component';
import { AppRoutingModule } from './app-routing.module';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    ListsComponent,
    MemberDetailComponent,
    MemberListComponent,
    MessagesComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FontAwesomeModule,
    BrowserAnimationsModule,    
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
