import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from '../app.component';
import { NavComponent } from '../nav/nav.component';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from '../home/home.component';
import { RegisterComponent } from '../register/register.component';
import { ListsComponent } from '../lists/lists.component';
import { MemberDetailComponent } from '../members/member-detail/member-detail.component';
import { MemberListComponent } from '../members/member-list/member-list.component';
import { MessagesComponent } from '../messages/messages.component';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared.module';


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
    FormsModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
