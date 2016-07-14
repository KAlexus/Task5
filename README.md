# Tasks

Create generator of random number sequence. Generator must return a sequence of integer numbers
from O to N, where N is a method`s parameter.
As a source of numbers use random.org API http://www.random.org/integers/?num=10&min=1&max=6&col=1&base=10&format=plain&rnd=new, where Max parameter is a max possible number,
returned by generator.
Method must cache result into a local queue and return numbers from it. If queue becomes empty –
make an async request to API to populate queue, and return numbers from BCL`s Random generator.
In case of API unavailability or error, stop making requests and return numbers from BCL`s generator
only. 
