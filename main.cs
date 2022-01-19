using System;
 
class Program
{
    enum Projections { XY, XZ, YZ };
    static void Main( string[] args )
    {
        const int N = 3;
        bool[,,] cube = new bool[ N, N, N ];
        Random r = new Random();
        for ( int i = 0; i < N; i++ )
            for ( int j = 0; j < N; j++ )
                for ( int l = 0; l < N; l++ )
                    cube[ i, j, l ] = r.Next( 0, 2 ) == 0 ? true : false;
 
        bool[,] tmp = Projection( cube, Projections.XY );
        for ( int i = 1; i < N - 1; i++ )
            for ( int j = 1; j < N - 1; j++ )
            {
                bool flag = true;
                if ( tmp[ i, j ] )
                    for ( int l = 0; l < N; l++ )
                        if ( cube[ i, j, l ] )
                        {
                            flag = false;
                            break;
                        }
 
                if ( flag )
                    Console.WriteLine( "Просвет существует.\nНачальные координаты ( {0}, {1}, {2} ), конечные ( {0}, {1}, {3} )", 
                        i + 1, j + 1, 1, N ); 
            }
 
        Console.ReadKey( true );
    }
 
    static bool[,] Projection( bool[ , , ] arr, Projections proj )
    {
        bool[,] projectionArray = new bool[ arr.GetLength( 0 ), arr.GetLength( 1 ) ];
        for ( int i = 0; i < arr.GetLength( 0 ); i++ )
            for ( int j = 0; j < arr.GetLength( 1 ); j++ )
                switch ( proj )
                {
                    case Projections.XY:
                        projectionArray[ i, j ] = arr[ i, j, 0 ];
                        break;
 
                    case Projections.XZ:
                        projectionArray[ i, j ] = arr[ i, 0, j ];
                        break;
 
                    case Projections.YZ:
                        projectionArray[ i, j ] = arr[ 0, i, j ];
                        break;
 
                }
 
        return projectionArray;
    }
 
    static void PrintArr( bool[ , ] arr )
    {
        for ( int i = 0; i < arr.GetLength( 0 ); i++ )
        {
            for ( int j = 0; j < arr.GetLength( 1 ); j++ )
                Console.Write( arr[ i, j ] + " " );
            Console.WriteLine();
        }
    }
}
