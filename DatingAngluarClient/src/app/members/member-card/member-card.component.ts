import { Component, Input, OnInit } from '@angular/core';
import { faEnvelope, faHeart, faUser } from '@fortawesome/free-solid-svg-icons';
import { Members } from 'src/app/models/members';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.scss']
})
export class MemberCardComponent implements OnInit {


  @Input() member: Members = {
    id: 0,
    username: '',
    gender: '',
    age: 0,
    knowsAs: undefined,
    created: '',
    lastActive: '',
    city: '',
    country: '',
    photoUrl: ''
  };
  
  // icons
  faUser = faUser;
  faHeart = faHeart;
  faEnvelope = faEnvelope;

  constructor() { }

  ngOnInit(): void {
  }

}
