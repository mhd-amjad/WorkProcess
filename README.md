# WorkProcess
# It is a web api sample project that uses some dapr building blocks and being triggered by zeebe engine.

The API is using dapr state management & Actors building blocks of dapr and triggered by local hosted zeebe engine included in self hosted Camunda platform.

To run this project:
- You have to install dapr on your machine.
- .Net 6 sdk.
- Clone and run Camunda platform on your machine by following these commands:
git clone https://github.com/camunda/camunda-platform.git
docker compose -f docker-compose-core.yaml up

You can access the local camunda dashboard and surf processes at: http://localhost:8081 .
You also can view or edit, deploy and run the model, It exists in the project folder.
