"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const discord_akairo_1 = require("discord-akairo");
class ReadyListener extends discord_akairo_1.Listener {
    constructor() {
        super('ready', {
            emitter: 'client',
            event: 'ready',
            category: 'client'
        });
    }
    exec() {
        console.log(`✔ ${this.client.user.tag} został włączony pomyślnie!`);
    }
}
exports.default = ReadyListener;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoicmVhZHkuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi8uLi9zcmMvbGlzdGVuZXJzL2NsaWVudC9yZWFkeS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOztBQUFBLG1EQUEwQztBQUUxQyxNQUFxQixhQUFjLFNBQVEseUJBQVE7SUFDL0M7UUFDSSxLQUFLLENBQUMsT0FBTyxFQUFFO1lBQ1gsT0FBTyxFQUFFLFFBQVE7WUFDakIsS0FBSyxFQUFFLE9BQU87WUFDZCxRQUFRLEVBQUUsUUFBUTtTQUNyQixDQUFDLENBQUM7SUFDUCxDQUFDO0lBRU0sSUFBSTtRQUVQLE9BQU8sQ0FBQyxHQUFHLENBQUMsS0FBSyxJQUFJLENBQUMsTUFBTSxDQUFDLElBQUksQ0FBQyxHQUFHLDZCQUE2QixDQUFDLENBQUM7SUFDeEUsQ0FBQztDQUNKO0FBYkQsZ0NBYUMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBMaXN0ZW5lciB9IGZyb20gJ2Rpc2NvcmQtYWthaXJvJztcclxuXHJcbmV4cG9ydCBkZWZhdWx0IGNsYXNzIFJlYWR5TGlzdGVuZXIgZXh0ZW5kcyBMaXN0ZW5lciB7XHJcbiAgICBwdWJsaWMgY29uc3RydWN0b3IoKSB7XHJcbiAgICAgICAgc3VwZXIoJ3JlYWR5Jywge1xyXG4gICAgICAgICAgICBlbWl0dGVyOiAnY2xpZW50JyxcclxuICAgICAgICAgICAgZXZlbnQ6ICdyZWFkeScsXHJcbiAgICAgICAgICAgIGNhdGVnb3J5OiAnY2xpZW50J1xyXG4gICAgICAgIH0pO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBleGVjKCk6IHZvaWQge1xyXG5cclxuICAgICAgICBjb25zb2xlLmxvZyhg4pyUICR7dGhpcy5jbGllbnQudXNlci50YWd9IHpvc3RhxYIgd8WCxIVjem9ueSBwb215xZtsbmllIWApO1xyXG4gICAgfVxyXG59Il19