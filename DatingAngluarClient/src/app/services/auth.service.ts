import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5002/api/Auth/'

  private currentUserSource = new BehaviorSubject<User | null >(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

   login(model: any){
    return this.http.post<User>(this.baseUrl + 'login', model).pipe(
      tap((res: User) => {
        const user = res;
        if(user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      }));    
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
