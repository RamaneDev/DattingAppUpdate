import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from '../app.component';
import { NavComponent } from '../nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from '../home/home.component';
import { RegisterComponent } from '../register/register.component';
import { ListsComponent } from '../lists/lists.component';
import { MemberDetailComponent } from '../members/member-detail/member-detail.component';
import { MemberListComponent } from '../members/member-list/member-list.component';
import { MessagesComponent } from '../messages/messages.component';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared.module';
import { NotFoundComponent } from '../errors/not-found/not-found.component';
import { ServerErrorComponent } from '../errors/server-error/server-error.component';
import { TestErrorsComponent } from '../errors/test-errors/test-errors.component';
import { ErrorInterceptor } from '../interceptors/error.interceptor';
import { MemberCardComponent } from '../members/member-card/member-card.component';
import { JwtInterceptor } from '../interceptors/jwt.interceptor';
import { LoadingInterceptor } from '../interceptors/loading.interceptor';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { PhotoEditorComponent } from '../members/photo-editor/photo-editor.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TextInputComponent } from '../forms/text-input/text-input.component';
import { DateInputComponent } from '../forms/date-input/date-input.component';
import { MemberMessagesComponent } from '../members/member-messages/member-messages.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    ListsComponent,
    MemberDetailComponent,
    MemberListComponent,
    MessagesComponent,
    NotFoundComponent,
    ServerErrorComponent,
    TestErrorsComponent,
    MemberCardComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    TextInputComponent,
    DateInputComponent,
    MemberMessagesComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,   
    FormsModule,
    SharedModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
