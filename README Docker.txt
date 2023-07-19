For UI you need to be located at 
LongRuning\LongRuning\ClientApp
Then run -> docker image build -t longruningui:1.0 .

Then run -> docker run --rm -d -p 80:80 longruningui:1.0


For API you need to be located at
LongRuning

Then run -> docker image build -t longruningapi:1.0 .\LongRuning

Then run -> docker container create --name longruningapi-container longruningapi:1.0 -p 7141:8080

Then run -> docker container start longruningapi-container