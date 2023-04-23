import { AfterViewChecked, AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Member } from 'src/app/models/member';
import { Message } from 'src/app/models/message';
import { MembersService } from 'src/app/services/members.service';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.scss']
})
export class MemberDetailComponent implements OnInit { 
  member!: Member;
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];

  activeTab!: TabDirective;
  messages: Message[] = [];  
  
  constructor(private memberService: MembersService, private route: ActivatedRoute, private messageService: MessageService ) {
  
   }



  ngOnInit(): void {
    this.getMember();  

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
  }


  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    for (const photo of this.member.photos) {
      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      })
    }
    return imageUrls;
  }

  getMember() {
    const username = this.route.snapshot.paramMap.get('username');
    if(username) {
      this.memberService.getMember(username).subscribe({
        next: user => {
          this.member = user;
          this.galleryImages = this.getImages();
     
        }
      });
    }   
  }

  onTabActivated(data: TabDirective) {    
      this.loadThreadMs();    
  }

  loadThreadMs() {
    this.messageService.getMessageThread(this.member.username).subscribe({
      next: msg => this.messages = msg
    })
  }


}
