import { Component, OnInit } from '@angular/core';
import { Members } from 'src/app/models/members';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {

  members: Members[] = [];

  constructor(private membersService: MembersService) { }

  ngOnInit(): void {
    this.getMembers();
    this.getMember();
  }

  getMembers() {
    this.membersService.getMembers().subscribe({
      next: resp => this.members = resp,
      error : err => console.log(err) 
    });
  }

  getMember() {
    this.membersService.getMember('Lola').subscribe({
      next: resp => console.log(resp),
      error : err => console.log(err) 
    });
  }

}
