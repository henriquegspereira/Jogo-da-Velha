using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("\n --- Tabuleiro Jogo da Velha --- \n");
        string[,] tabuleiro = CriarTabuleiro();
        string JogadorAtual = "x";
        int qntdJogadas = 0;

        while (true)
        {
            DesenharTabuleiro(tabuleiro);

            Console.Write($"\nJogador {JogadorAtual}, Escolha uma posição de 1-9: ");

            // Fiz com auxilio do gemini para entender a lógica
            int pos;
            while (!int.TryParse(Console.ReadLine(), out pos) || pos < 1 || pos > 9)
            {
                Console.WriteLine("\nEntrada inválida! Por favor, digite um número de 1 a 9");
                Console.Write($"Jogador {JogadorAtual}, escolha outra posição: ");
            }
            // -------------------------------------------------------------------------------

            int linha = (pos - 1) / 3;
            int coluna = (pos - 1) % 3;

            if (tabuleiro[linha, coluna] == "x" || tabuleiro[linha, coluna] == "o")
            {
                Console.WriteLine("\n-- Posição ocupada, escolha outra! --\n");
                continue;
            }

            tabuleiro[linha, coluna] = JogadorAtual;

            qntdJogadas++;

            string vencedor = VerificarVencedor(tabuleiro);

            if (vencedor != "")
            {
                DesenharTabuleiro(tabuleiro);
                Console.WriteLine($"\n-- O Jogador {vencedor}, Venceu a partida! -- \n");
                break;
            }
            else if (qntdJogadas == 9)
            {
                Console.WriteLine("\n-- Velha. A partida empatou!--\n");
                break;
            }

            if (JogadorAtual == "x")
            {
                JogadorAtual = "o";
            }
            else
            {
                JogadorAtual = "x";
            }
        }
    }
    static string[,] CriarTabuleiro()
    {
        string[,] tabuleiro = new string[3, 3];
        int contador = 1;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tabuleiro[i, j] = contador.ToString();
                contador++;
            }
        }
        return tabuleiro;
    }
    static void DesenharTabuleiro(string[,] b)
    {
        Console.Clear();
        Console.WriteLine($" {b[0, 0]} | {b[0, 1]} | {b[0, 2]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {b[1, 0]} | {b[1, 1]} | {b[1, 2]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {b[2, 0]} | {b[2, 1]} | {b[2, 2]} ");
    }
    static string VerificarVencedor(string[,] b)
    {
        for (int i = 0; i < 3; i++)
        {
            if (b[i, 0] == b[i, 1] && b[i, 1] == b[i, 2])
            {
                return (b[i, 0]);
            }
        }

        for (int j = 0; j < 3; j++)
        {
            if (b[0, j] == b[1, j] && b[1, j] == b[2, j])
            {
                return (b[0, j]);
            }
        }

        if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
        {
            return (b[0, 0]);
        }

        if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
        {
            return (b[0, 2]);
        }

        return "";
    }
}

// Pontos de melhoria: A mensagem Console.WriteLine("\n-- Posição ocupada, escolha outra! --\n"); 
// não é exibida, pois quando cai nessa condição ele continua o loop e chama a função desenhar
//e a primeira coisa dessa função é limpar; logo que a mensagem é exibida ja é apagada.