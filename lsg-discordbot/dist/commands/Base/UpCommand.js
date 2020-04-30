"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const discord_akairo_1 = require("discord-akairo");
class UpCommand extends discord_akairo_1.Command {
    constructor() {
        super('up', {
            aliases: ['up'],
            category: 'Base',
            description: {
                content: 'Sprawdza czy bot jest aktywny',
                examples: ['up'],
                usages: 'up'
            },
            ratelimit: 3
        });
    }
    exec(message) {
        return message.util.reply(`up został wykonany w: ${this.client.ws.ping}ms`);
    }
}
exports.default = UpCommand;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiVXBDb21tYW5kLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vLi4vc3JjL0NvbW1hbmRzL0Jhc2UvVXBDb21tYW5kLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7O0FBQUEsbURBQXlDO0FBR3pDLE1BQXFCLFNBQVUsU0FBUSx3QkFBTztJQUMxQztRQUNJLEtBQUssQ0FBQyxJQUFJLEVBQUU7WUFDUixPQUFPLEVBQUUsQ0FBQyxJQUFJLENBQUM7WUFDZixRQUFRLEVBQUUsTUFBTTtZQUNoQixXQUFXLEVBQUU7Z0JBQ1QsT0FBTyxFQUFFLCtCQUErQjtnQkFDeEMsUUFBUSxFQUFFLENBQUMsSUFBSSxDQUFDO2dCQUNoQixNQUFNLEVBQUUsSUFBSTthQUNmO1lBQ0QsU0FBUyxFQUFFLENBQUM7U0FDZixDQUFDLENBQUM7SUFDUCxDQUFDO0lBR00sSUFBSSxDQUFDLE9BQWdCO1FBQ3hCLE9BQU8sT0FBTyxDQUFDLElBQUksQ0FBQyxLQUFLLENBQUMseUJBQXlCLElBQUksQ0FBQyxNQUFNLENBQUMsRUFBRSxDQUFDLElBQUksSUFBSSxDQUFDLENBQUM7SUFDaEYsQ0FBQztDQUNKO0FBbEJELDRCQWtCQyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IENvbW1hbmQgfSBmcm9tICdkaXNjb3JkLWFrYWlybyc7XHJcbmltcG9ydCB7IE1lc3NhZ2UgfSBmcm9tICdkaXNjb3JkLmpzJztcclxuXHJcbmV4cG9ydCBkZWZhdWx0IGNsYXNzIFVwQ29tbWFuZCBleHRlbmRzIENvbW1hbmQge1xyXG4gICAgcHVibGljIGNvbnN0cnVjdG9yKCkge1xyXG4gICAgICAgIHN1cGVyKCd1cCcsIHtcclxuICAgICAgICAgICAgYWxpYXNlczogWyd1cCddLFxyXG4gICAgICAgICAgICBjYXRlZ29yeTogJ0Jhc2UnLFxyXG4gICAgICAgICAgICBkZXNjcmlwdGlvbjoge1xyXG4gICAgICAgICAgICAgICAgY29udGVudDogJ1NwcmF3ZHphIGN6eSBib3QgamVzdCBha3R5d255JyxcclxuICAgICAgICAgICAgICAgIGV4YW1wbGVzOiBbJ3VwJ10sXHJcbiAgICAgICAgICAgICAgICB1c2FnZXM6ICd1cCdcclxuICAgICAgICAgICAgfSxcclxuICAgICAgICAgICAgcmF0ZWxpbWl0OiAzXHJcbiAgICAgICAgfSk7XHJcbiAgICB9XHJcblxyXG5cclxuICAgIHB1YmxpYyBleGVjKG1lc3NhZ2U6IE1lc3NhZ2UpIHtcclxuICAgICAgICByZXR1cm4gbWVzc2FnZS51dGlsLnJlcGx5KGB1cCB6b3N0YcWCIHd5a29uYW55IHc6ICR7dGhpcy5jbGllbnQud3MucGluZ31tc2ApO1xyXG4gICAgfVxyXG59Il19