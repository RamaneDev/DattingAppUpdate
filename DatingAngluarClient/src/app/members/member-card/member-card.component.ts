import { Component, Input, OnInit } from '@angular/core';
import { faEnvelope, faHeart, faUser } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { Members } from 'src/app/models/members';
import { MembersService } from 'src/app/services/members.service';

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

  constructor(private memberService: MembersService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  Like(member: Members) {
    this.memberService.Like(member.username).subscribe({
      next: () => this.toastr.success('You have like ' + member.knowsAs)
    });
  }

}
