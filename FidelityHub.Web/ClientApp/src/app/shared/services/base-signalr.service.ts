import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
  LogLevel,
} from "@microsoft/signalr";
import { MessagePackHubProtocol } from "@microsoft/signalr-protocol-msgpack";
import { AuthenticationService } from "./authentication.service";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class BaseSignalrService {
  public hubConnection: HubConnection;

  constructor(private auth: AuthenticationService) {}

  // --- Connection Support ---
  public startConnection(hub: string = "signalr"): Observable<boolean> {
    return new Observable((observer) => {
      if (this.auth.isLoggedIn) {
        this.hubConnection = this.getConnection(hub);
        this.hubConnection
          .start()
          .then(() => {
            if (!environment.production) {
              console.log("Connection started");
            }
            observer.next(true);
            observer.complete();
          })
          .catch((err) => {
            if (!environment.production) {
              console.log(
                "Error while establishing signalr connection: " + err
              );
            }
            observer.next(false);
            observer.complete();
          });
      } else {
        console.log("Not logged in... What are you trying to do?");
        observer.next(false);
        observer.complete();
      }
    });
  }

  public stopConnection() {
    this.hubConnection
      .stop()
      .then((response) => {
        console.log("Closing Hub Connection");
      })
      .catch((err) => {
        console.log(`Error Closing Hub Connection ${err}`);
      });
  }

  private getConnection(hub: string): HubConnection {
    return (
      new HubConnectionBuilder()
        .withUrl(`${environment.apiPath}`)
        //.withUrl(
        // `${
        //  environment.apiPath
        //}/${hub}?access_token=${this.auth.getAuthToken()}`
        //)
        //.withHubProtocol(new MessagePackHubProtocol())
        .configureLogging(LogLevel.Error)
        .build()
    );
  }

  // --- Getters ---
  get isHubConnected(): boolean {
    if (!this.hubConnection) {
      return false;
    }
    return this.hubConnection.state == HubConnectionState.Connected;
  }
}
