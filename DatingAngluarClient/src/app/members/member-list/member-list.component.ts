import { Component, OnInit } from '@angular/core';
import { Members } from 'src/app/models/members';
import { Pagination } from 'src/app/models/pagination';
import { User } from 'src/app/models/user';
import { UserParams } from 'src/app/models/userParams';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {

  members: Members[] | null = [];
  pagination!: Pagination | null;
  userParams!: UserParams;
  user!: User;
  genderList = [{ value: 'male', display: 'Males'}, { value: 'female', display: 'Females'}];

  constructor(private membersService: MembersService) {
    this.userParams = this.membersService.getUserParams();
   }

  ngOnInit(): void {     
      this.getMembers();    
  }

  getMembers() {
    this.membersService.setUserParams(this.userParams);
    this.membersService.getMembers(this.userParams).subscribe({
      next:res => {
          this.members = res.result;
          this.pagination = res.pagination;
      },
      error : err => console.log(err) 
    });
  }

  getMember() {
    this.membersService.getMember('Lola').subscribe({
      next: resp => console.log(resp),
      error : err => console.log(err) 
    });
  }

  resetFilters() {
    this.userParams = this.membersService.resetUserParams();
    this.getMembers();
  }

  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.membersService.setUserParams(this.userParams);
    this.getMembers();
  }

}
