import { Component } from '@angular/core';
import { ResolveEnd, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

type AppMode = 'loading' | 'active' | 'login' | 'external';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(
    private router: Router,
  ) {
  }

  mode = new BehaviorSubject<AppMode>('loading');

  setMode(mode: AppMode) {
    if (this.mode.getValue() == mode)
      return;
    this.mode.next(mode);
    console.log("AppMode Switch: " + mode);
  }

  ngOnInit(): void {
    this.router.events.forEach((event) => {
      if (event instanceof ResolveEnd) {
        var url = event.url;
        if (url.startsWith('/external')) {
          this.setMode('external');        
        }
        else if (url.startsWith('/login') || url.startsWith('/forgotten-password') || url.startsWith('/reset-password')) {
          this.setMode('login');
        }
        else {
          this.setMode('active');        
        }
      }
    });
  }
}
