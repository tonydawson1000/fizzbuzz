# fizzbuzz
FizzBuzz Rules

Iterate through a range of numbers, typically 1 to 100.

For each number:

If the number is divisible by 3, print "Fizz".

If the number is divisible by 5, print "Buzz".

If the number is divisible by both 3 and 5 (i.e., divisible by 15), print "FizzBuzz".

Otherwise, print the number itself.

# Build Container Image
1. Build the Containerfile

    `podman build -t quay.io/lab/fizzbuzz-api .`

1. Run Single Container

    `podman run -p 8080:8080 quay.io/lab/fizzbuzz-api`

1. Build for alternative CPU Architectures - and associate with Manifest

    1. Build a `linux-amd64` variant Image

        `podman build -t quay.io/lab/fizzbuzz-api:linux-amd64 --platform linux/amd64 --manifest quay.io/lab/fizzbuzz-api .`

    1. Build a `linux-arm64` variant Image

        `podman build -t quay.io/lab/fizzbuzz-api:linux-arm64 --platform linux/arm64 --manifest quay.io/lab/fizzbuzz-api .`

1. Inspect the Manifest

    `podman manifest inspect quay.io/lab/fizzbuzz-api`