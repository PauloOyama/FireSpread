// int radius = 1;
// int vector_length = 10;


// int transition_state_length = 1 + (2 * radius);


// List<int> reticulado_flag = new();
// List<int> reticulado = new();

// for (int i = 0; i < vector_length; i++)
// {
//     Random RNG = new();
//     reticulado_flag.Add(RNG.Next(0, 2));
// }

// Console.WriteLine("[{0}]", string.Join(", ", reticulado_flag));

// //add padding
// reticulado.Add(reticulado_flag[^1]);
// reticulado_flag.ForEach(reticulado.Add);
// reticulado.Add(reticulado_flag[0]);


// Console.WriteLine("\n with Padding \n");
// Console.WriteLine("[{0}]", string.Join(", ", reticulado));

// List<int> transition_states = new();
// for (int i = 0; i < Math.Pow(2, transition_state_length); i++)
// {
//     Random RNG = new();
//     transition_states.Add(RNG.Next(0, 2));
// }

// Console.WriteLine(2 ^ transition_state_length);
// Console.WriteLine("\nTransitiom States \n");
// Console.WriteLine("[{0}]", string.Join(", ", transition_states));


