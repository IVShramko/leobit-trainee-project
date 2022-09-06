import { HubConnection } from '@microsoft/signalr';
export abstract class SignalRBase {

    connection: HubConnection;

    constructor() {
        setTimeout(()=> {
            console.log('super')

        }, 2000)
    }
}