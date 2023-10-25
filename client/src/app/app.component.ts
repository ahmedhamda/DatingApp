import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'client';
  users:any;

  constructor(private http:HttpClient){

  }
  ngOnInit(): void {
this.http.get('http://localhost:5284/api/Users').subscribe({
next:response=> this.users=response,
error: error=> console.log(error),
complete:() =>console.log('Request has completed')

})

}
}
