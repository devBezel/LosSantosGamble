"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const discord_akairo_1 = require("discord-akairo");
const TrelloModule_1 = require("../../../Modules/TrelloModule");
const Config_1 = require("../../../Config");
class TrelloBoardCommand extends discord_akairo_1.Command {
    constructor() {
        super('tablica', {
            aliases: ['tablica'],
            category: 'Trello',
            description: {
                content: 'Pokazuje tablice trello',
                examples: ['tablica'],
                usages: 'tablica'
            },
            ratelimit: 3
        });
    }
    //id listy 5df80be6b781864669ab1893
    async exec(message) {
        await TrelloModule_1.createTrelloList('Testowa lista', Config_1.trelloBoardId);
    }
}
exports.default = TrelloBoardCommand;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiVHJlbGxvQm9hcmRDb21tYW5kLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vLi4vLi4vc3JjL0NvbW1hbmRzL0Jhc2UvVHJlbGxvQ29tbWFuZHMvVHJlbGxvQm9hcmRDb21tYW5kLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7O0FBQUEsbURBQXlDO0FBRXpDLGdFQUFtRztBQUVuRyw0Q0FBK0M7QUFFL0MsTUFBcUIsa0JBQW1CLFNBQVEsd0JBQU87SUFDbkQ7UUFDSSxLQUFLLENBQUMsU0FBUyxFQUFFO1lBQ2IsT0FBTyxFQUFFLENBQUMsU0FBUyxDQUFDO1lBQ3BCLFFBQVEsRUFBRSxRQUFRO1lBQ2xCLFdBQVcsRUFBRTtnQkFDVCxPQUFPLEVBQUUseUJBQXlCO2dCQUNsQyxRQUFRLEVBQUUsQ0FBQyxTQUFTLENBQUM7Z0JBQ3JCLE1BQU0sRUFBRSxTQUFTO2FBQ3BCO1lBQ0QsU0FBUyxFQUFFLENBQUM7U0FDZixDQUFDLENBQUM7SUFDUCxDQUFDO0lBRUQsbUNBQW1DO0lBQzVCLEtBQUssQ0FBQyxJQUFJLENBQUMsT0FBZ0I7UUFDOUIsTUFBTSwrQkFBZ0IsQ0FBQyxlQUFlLEVBQUUsc0JBQWEsQ0FBQyxDQUFDO0lBQzNELENBQUM7Q0FDSjtBQWxCRCxxQ0FrQkMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBDb21tYW5kIH0gZnJvbSAnZGlzY29yZC1ha2Fpcm8nO1xyXG5pbXBvcnQgeyBNZXNzYWdlIH0gZnJvbSAnZGlzY29yZC5qcyc7XHJcbmltcG9ydCB7IGxpc3RSZXF1ZXN0LCBnZXRUcmVsbG9Cb2FyZExpc3RzLCBjcmVhdGVUcmVsbG9MaXN0IH0gZnJvbSAnLi4vLi4vLi4vTW9kdWxlcy9UcmVsbG9Nb2R1bGUnO1xyXG5pbXBvcnQgVHJlbGxvSGVscGVyIGZyb20gJy4uLy4uLy4uL0hlbHBlcnMvVHJlbGxvSGVscGVyJztcclxuaW1wb3J0IHsgdHJlbGxvQm9hcmRJZCB9IGZyb20gJy4uLy4uLy4uL0NvbmZpZydcclxuXHJcbmV4cG9ydCBkZWZhdWx0IGNsYXNzIFRyZWxsb0JvYXJkQ29tbWFuZCBleHRlbmRzIENvbW1hbmQge1xyXG4gICAgY29uc3RydWN0b3IoKSB7XHJcbiAgICAgICAgc3VwZXIoJ3RhYmxpY2EnLCB7XHJcbiAgICAgICAgICAgIGFsaWFzZXM6IFsndGFibGljYSddLFxyXG4gICAgICAgICAgICBjYXRlZ29yeTogJ1RyZWxsbycsXHJcbiAgICAgICAgICAgIGRlc2NyaXB0aW9uOiB7XHJcbiAgICAgICAgICAgICAgICBjb250ZW50OiAnUG9rYXp1amUgdGFibGljZSB0cmVsbG8nLFxyXG4gICAgICAgICAgICAgICAgZXhhbXBsZXM6IFsndGFibGljYSddLFxyXG4gICAgICAgICAgICAgICAgdXNhZ2VzOiAndGFibGljYSdcclxuICAgICAgICAgICAgfSxcclxuICAgICAgICAgICAgcmF0ZWxpbWl0OiAzXHJcbiAgICAgICAgfSk7XHJcbiAgICB9XHJcblxyXG4gICAgLy9pZCBsaXN0eSA1ZGY4MGJlNmI3ODE4NjQ2NjlhYjE4OTNcclxuICAgIHB1YmxpYyBhc3luYyBleGVjKG1lc3NhZ2U6IE1lc3NhZ2UpIHtcclxuICAgICAgICBhd2FpdCBjcmVhdGVUcmVsbG9MaXN0KCdUZXN0b3dhIGxpc3RhJywgdHJlbGxvQm9hcmRJZCk7XHJcbiAgICB9XHJcbn0iXX0=