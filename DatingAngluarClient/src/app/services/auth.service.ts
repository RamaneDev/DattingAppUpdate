import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5002/api/Auth/';

  BaseUrl = environment.apiUrl + '/Auth/';

  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

   login(model: any){
    return this.http.post<User>(this.baseUrl + 'login', model).pipe(
      tap((res: User) => {
        const user = res;
        if(user) {
          this.setCurrentUser(user);
        }
      }));    
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'register', model).pipe(
      map((user: User) => {
        if(user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
