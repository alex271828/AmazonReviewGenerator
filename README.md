# AmazonReviewGenerator

Generates random review text and summary based on existing reviews using Markov chains.

Deployed at https://alexo-amazonreviewgenerator.azurewebsites.net//API/generate

Or use it as container: ghcr.io/alex271828/amazonreviewgenerator:main

Docker command: docker pull ghcr.io/alex271828/amazonreviewgenerator:main

Docker command using default data: docker run -it --rm -p 63080:80 ghcr.io/alex271828/amazonreviewgenerator:main

Docker command using custom data: docker run -it --rm -p 63080:80 -v /home/alex/data:/app/data ghcr.io/alex271828/amazonreviewgenerator:main

Very User Friendly Front End: https://alexo-amazonreviewgenerator.azurewebsites.net/userFriendlyFrontEnd.html

# Kind of completed, but there are still TODOs

